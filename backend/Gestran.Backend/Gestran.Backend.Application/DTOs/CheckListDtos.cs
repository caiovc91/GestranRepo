using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Application.DTOs
{
    public record CheckListItemCreateDto(Guid ItemTypeId, string? ItemTypeName, bool? IsChecked, string? Comments);
    public record CheckListCreateDto(string CheckListName, Guid? CollectionId, string? GeneralComments, List<CheckListItemCreateDto> CheckListItems);

    public record CheckListItemUpdateDto(Guid Id, bool? IsChecked, string? Comments);
    public record CheckListUpdateDto(string? CheckListName, string? GeneralComments, List<CheckListItemUpdateDto>? CheckListItems, bool? InProgress);

    // Response DTOs
    public record CheckListItemResponseDto(Guid Id, Guid ItemTypeId, string? ItemTypeName, bool? IsChecked, string? Comments);
    public record CheckListResponseDto(
        Guid Id,
        string CheckListName,
        Guid? ExecutedById,
        Guid? CollectionId,
        string? ExecutedByName,
        string CurrentStatus,
        bool InProgress,
        bool IsApproved,
        DateTime CreationDate,
        DateTime? LastUpdateDate,
        DateTime? ApprovalDate,
        string? GeneralComments,
        List<CheckListItemResponseDto> CheckListItems
    );

}
