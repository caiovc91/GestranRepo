import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { CheckListResponseDto, CheckListItemUpdateDto } from '../models/checklist.model';

@Injectable({
  providedIn: 'root'
})
export class ChecklistsService {
  constructor(private api: ApiService) {}

  async getAll(): Promise<CheckListResponseDto[]> {
    return this.api.get<CheckListResponseDto[]>('/CheckList');
  }

  async getById(id: string): Promise<CheckListResponseDto> {
    return this.api.get<CheckListResponseDto>(`/CheckList/${id}`);
  }

  async updateItem(checklistId: string, item: CheckListItemUpdateDto) {
    return this.api.post(`/CheckList/${checklistId}/UpdateItems`, { items: [item] });
  }

  async finishChecklist(checklistId: string) {
    return this.api.post(`/CheckList/${checklistId}/FinishExecution`, {});
  }

  async approveChecklist(checklistId: string, approve: boolean) {
    return this.api.post(`/CheckList/${checklistId}/Approve`, { approve });
  }
}