import { Component, OnInit } from '@angular/core';
import { ChecklistsService } from '../services/checklists.service';
import { CheckListResponseDto } from '../models/checklist.model';

@Component({
  selector: 'app-approval',
  templateUrl: './approval.page.html'
})
export class ApprovalPage implements OnInit {
  checklists: CheckListResponseDto[] = [];

  constructor(private checklistsService: ChecklistsService) {}

  ngOnInit() {
    this.loadChecklists();
  }

  async loadChecklists() {
    this.checklists = await this.checklistsService.getAll();
  }

  async approve(id: string) {
    await this.checklistsService.approveChecklist(id, true);
    this.loadChecklists();
  }

  async reject(id: string) {
    await this.checklistsService.approveChecklist(id, false);
    this.loadChecklists();
  }
}