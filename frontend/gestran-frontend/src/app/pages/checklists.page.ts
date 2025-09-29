import { ApiService } from '../services/api.service';
import { SessionService } from '../services/session.service';
import { Router } from '../router';

export class CheckListsPage {
  static async render(root: HTMLElement) {
    if (!SessionService.isLoggedIn()) { Router.load('/login'); return; }

    root.innerHTML = `
      <h2>Checklists</h2>
      <button class="btn btn-success mb-2" id="create-checklist-btn">Add Checklist</button>
      <table class="table" id="checklists-table">
        <thead><tr><th>Name</th><th>Status</th><th>In Progress</th><th>Approved</th><th>Actions</th></tr></thead>
        <tbody></tbody>
      </table>
    `;

    document.getElementById('create-checklist-btn')!.onclick = () => {
      window.history.pushState({}, '', '/checklist-form');
      Router.load('/checklist-form');
    };

    try {
      const controller = new AbortController();
      const checklists = await ApiService.getCheckLists(controller.signal);
      const tbody = document.querySelector('#checklists-table tbody')!;
      checklists.forEach((c: any) => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
          <td>${c.checkListName}</td>
          <td>${c.currentStatus}</td>
          <td>${c.inProgress}</td>
          <td>${c.isApproved}</td>
          <td>
            <button class="btn btn-primary btn-sm edit-btn">Edit</button>
            <button class="btn btn-danger btn-sm delete-btn">Delete</button>
          </td>
        `;
        tr.querySelector('.edit-btn')!.addEventListener('click', () => {
          window.history.pushState({}, '', `/checklist-form?id=${c.id}`);
          Router.load(`/checklist-form?id=${c.id}`);
        });
        tr.querySelector('.delete-btn')!.addEventListener('click', async () => {
          if (confirm('Are you sure?')) {
            await ApiService.deleteCheckList(c.id);
            CheckListsPage.render(root);
          }
        });
        tbody.appendChild(tr);
      });
    } catch {
      root.innerHTML += `<p class="text-danger">Failed to load checklists.</p>`;
    }
  }
}