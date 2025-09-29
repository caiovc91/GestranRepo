import { ApiService } from '../services/api.service';
import { SessionService } from '../services/session.service';
import { Router } from '../router';

export class CheckListFormPage {
  static async render(root: HTMLElement, params: URLSearchParams) {
    if (!SessionService.isLoggedIn()) { Router.load('/login'); return; }

    let checklist: any = { checkListName: '', checkListItems: [] };
    const id = params.get('id');
    if (id) {
      const controller = new AbortController();
      const data = await ApiService.getCheckLists(controller.signal);
      checklist = data.find((c: any) => c.id === id) || checklist;
    }

    const itemTypes = await ApiService.getCheckListItemTypes();

    root.innerHTML = `
      <h2>${id ? 'Edit' : 'Create'} Checklist</h2>
      <div class="mb-3"><label>Name</label><input class="form-control" id="checklist-name" value="${checklist.checkListName}"></div>
      <div id="items-container"></div>
      <button class="btn btn-success" id="save-btn">Save</button>
    `;

    const itemsContainer = document.getElementById('items-container')!;
    itemTypes.forEach((t: any) => {
      const checked = checklist.checkListItems?.some((i: any) => i.itemTypeId === t.id && i.isChecked) ? 'checked' : '';
      const div = document.createElement('div');
      div.className = 'form-check';
      div.innerHTML = `<input class="form-check-input" type="checkbox" id="item-${t.id}" ${checked}>
                       <label class="form-check-label">${t.typeName}</label>`;
      itemsContainer.appendChild(div);
    });

    document.getElementById('save-btn')!.onclick = async () => {
      const name = (document.getElementById('checklist-name') as HTMLInputElement).value;
      const selectedItems = itemTypes
        .filter((t: any) => (document.getElementById(`item-${t.id}`) as HTMLInputElement).checked)
        .map((t: any) => ({ itemTypeId: t.id, itemTypeName: t.typeName, isChecked: true }));

      const payload = { ...checklist, checkListName: name, checkListItems: selectedItems };

      if (id) await ApiService.updateCheckList(id, payload);
      else await ApiService.createCheckList(payload);

      window.history.pushState({}, '', '/checklists');
      Router.load('/checklists');
    };
  }
}