import { ChecklistExecutionPage } from './pages/checklist-execution.page';
import { ApprovalPage } from './pages/approval.page';
import { HeaderComponent } from './components/header.component';

@NgModule({
  declarations: [
    ChecklistExecutionPage,
    ApprovalPage,
    HeaderComponent,
    // ... outros componentes
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(AppRoutes),
    HttpClientModule,
    FormsModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}