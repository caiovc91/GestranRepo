import { ApiService } from '../services/api.service';
import { SessionService } from '../services/session.service';
import { Router } from '../router';

export class CheckListItemTypeFormPage {
  static async render(root: HTMLElement, params: URLSearchParams) {
    if (!SessionService.isLoggedIn()) { Router.load('/login'); return; }

    let type: any = { typeName: '', description: '', isEnabled: true };
    const id = params.get('id');
    if (id) {
      const controller = new AbortController();
      const types = await ApiService.getCheckListItemTypes(controller.signal);
      type = types.find((t: any) => t.id === id) || type;
    }

    root.innerHTML = `
      <h2>${id ? 'Edit' : 'Create'} Checklist Item Type</h2>
      <div class="mb-3"><label>Name</label><input class="form-control" id="type-name" value="${type.typeName}"></div>
      <div class="mb-3"><label>Description</label><input class="form-control" id="type-desc" value="${type.description}"></div>
      <div class="form-check mb-3">
        <input class="form-check-input" type="checkbox" id="type-enabled" ${type.isEnabled ? 'checked' : ''}>
        <label class="form-check-label">Enabled</label>
      </div>
      <button class="btn btn-success" id="save-btn">Save</button>
    `;

    document.getElementById('save-btn')!.onclick = async () => {
      const payload = {
        typeName: (document.getElementById('type-name') as HTMLInputElement).value,
        description: (document.getElementById('type-desc') as HTMLInputElement).value,
        isEnabled: (document.getElementById('type-enabled') as HTMLInputElement).checked
      };

      if (id) await ApiService.updateCheckListItemType(id, payload);
      else await ApiService.createCheckListItemType(payload);

      window.history.pushState({}, '', '/checklist-item-types');
      Router.load('/checklist-item-types');
    };
  }
}