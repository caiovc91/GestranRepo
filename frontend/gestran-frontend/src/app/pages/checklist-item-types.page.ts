import { ApiService } from '../services/api.service';
import { SessionService } from '../services/session.service';
import { Router } from '../router';

export class CheckListItemTypesPage {
  static async render(root: HTMLElement) {
    if (!SessionService.isLoggedIn()) { Router.load('/login'); return; }

    root.innerHTML = `
      <h2>Checklist Item Types</h2>
      <button class="btn btn-success mb-2" id="create-btn">Add Item Type</button>
      <table class="table" id="types-table">
        <thead><tr><th>Name</th><th>Description</th><th>Enabled</th><th>Actions</th></tr></thead>
        <tbody></tbody>
      </table>
    `;

    document.getElementById('create-btn')!.onclick = () => {
      window.history.pushState({}, '', '/checklist-item-type-form');
      Router.load('/checklist-item-type-form');
    };

    const controller = new AbortController();
    const types = await ApiService.getCheckListItemTypes(controller.signal);
    const tbody = document.querySelector('#types-table tbody')!;
    types.forEach((t: any) => {
      const tr = document.createElement('tr');
      tr.innerHTML = `<td>${t.typeName}</td><td>${t.description}</td><td>${t.isEnabled}</td>
                      <td>
                        <button class="btn btn-primary btn-sm edit-btn">Edit</button>
                        <button class="btn btn-danger btn-sm delete-btn">Delete</button>
                      </td>`;
      tr.querySelector('.edit-btn')!.addEventListener('click', () => {
        window.history.pushState({}, '', `/checklist-item-type-form?id=${t.id}`);
        Router.load(`/checklist-item-type-form?id=${t.id}`);
      });
      tr.querySelector('.delete-btn')!.addEventListener('click', async () => {
        if (confirm('Are you sure?')) {
          await ApiService.deleteCheckListItemType(t.id);
          CheckListItemTypesPage.render(root);
        }
      });
      tbody.appendChild(tr);
    });
  }
}