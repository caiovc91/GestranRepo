import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { CheckListItemTypeCreateDto } from '../models/checklist-item-type.model';

@Injectable({ providedIn: 'root' })
export class ChecklistItemTypesService {
    constructor(private api: ApiService) {}

    getAll() { return this.api.get('/checklistitemtype'); }
    create(dto: CheckListItemTypeCreateDto) { return this.api.post('/checklistitemtype', dto); }
    delete(id: string) { return this.api.delete(`/checklistitemtype/${id}`); }
}