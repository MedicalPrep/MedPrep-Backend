namespace MedPrep.Api.Models.Common;

public interface ISoftDeletable
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public void UndoDelete()
    {
        this.IsDeleted = false;
        this.DeletedAt = null;
    }
}
