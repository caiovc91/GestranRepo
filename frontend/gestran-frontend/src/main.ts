import 'zone.js';
import { Router } from './app/router';
import { SessionService } from './app/services/session.service';

// Inicializa a aplicação
document.addEventListener('DOMContentLoaded', () => {
  window.addEventListener('popstate', () => Router.load(window.location.pathname));
  Router.load(window.location.pathname);
});