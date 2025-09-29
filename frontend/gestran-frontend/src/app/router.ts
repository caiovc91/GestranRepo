import { LoginPage } from './pages/login.page';
import { UsersPage } from './pages/users.page';
import { CheckListsPage } from './pages/checklists.page';
import { CheckListFormPage } from './pages/checklist-form.page';
import { CheckListItemTypesPage } from './pages/checklist-item-types.page';
import { CheckListItemTypeFormPage } from './pages/checklist-item-type-form.page';

export class Router {
  static routes: { path: string, page: any }[] = [
    { path: '/login', page: LoginPage },
    { path: '/users', page: UsersPage },
    { path: '/checklists', page: CheckListsPage },
    { path: '/checklist-form', page: CheckListFormPage },
    { path: '/checklist-item-types', page: CheckListItemTypesPage },
    { path: '/checklist-item-type-form', page: CheckListItemTypeFormPage },
  ];

  static load(url: string) {
    const root = document.getElementById('app-root')!;
    const [path, query] = url.split('?');
    const params = new URLSearchParams(query);
    const route = Router.routes.find(r => r.path === path) || { page: LoginPage };
    route.page.render(root, params);
  }
}