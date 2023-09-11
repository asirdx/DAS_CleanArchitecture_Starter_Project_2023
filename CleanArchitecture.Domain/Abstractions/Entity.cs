namespace CleanArchitecture.Domain.Abstractions;

public abstract class Entity
{
    public Entity()// her nesne oluturulduğunda string e çevrilmiş bir GUID oluşturulup Id alanına verilecek.
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; } // this is Guid but converted to string for best prctice from the very beginning
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set;}
}
