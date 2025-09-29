import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChecklistsService } from '../services/checklists.service';
import { CheckListResponseDto, CheckListItemResponseDto } from '../models/checklist.model';

@Component({
  selector: 'app-checklist-execution',
  templateUrl: './checklist-execution.page.html',
})
export class ChecklistExecutionPage implements OnInit {
  checklist: CheckListResponseDto | null = null;
  canFinish: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private checklistsService: ChecklistsService
  ) {}

  ngOnInit(): void {
    const checklistId = this.route.snapshot.params['id'];
    this.loadChecklist(checklistId);
  }

  async loadChecklist(id: string) {
    this.checklist = await this.checklistsService.getById(id);
    this.updateFinishState();
  }

  async markItem(itemId: string) {
    if (!this.checklist) return;

    const item = this.checklist.CheckListItems.find(i => i.Id === itemId);
    if (!item) return;

    // Atualiza no backend
    await this.checklistsService.updateItem(this.checklist.Id, itemId, true);

    // Atualiza localmente
    item.IsChecked = true;
    this.updateFinishState();
  }

  updateFinishState() {
    if (!this.checklist) return;

    const anyUnchecked = this.checklist.CheckListItems.some(i => !i.IsChecked);
    this.canFinish = !anyUnchecked;
  }

  async finishChecklist() {
    if (!this.checklist) return;

    await this.checklistsService.finishChecklist(this.checklist.Id);
    alert('Checklist finished successfully!');
    // Pode navegar para outra página se quiser
  }
}