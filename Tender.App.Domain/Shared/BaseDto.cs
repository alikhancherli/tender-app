using System.ComponentModel.DataAnnotations;

namespace Tender.App.Domain.Shared;

public abstract record BaseDto<TId> where TId : struct, IEquatable<TId>
{
    [Display(Name = "شناسه")]
    public TId Id { get; init; }
    [Display(Name = "تاریخ ایجاد")]
    public DateTimeOffset CreatedTimeUtc { get; init; }
    [Display(Name = "تاریخ ویرایش")]
    public DateTimeOffset? ModifiedTimeUtc { get; init; }
}
