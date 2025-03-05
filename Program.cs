var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
List<Bike> repo = new List<Bike>()
{
    new Bike(1, "Горный_велосипед", "Хороший велосипед, прекрасно ездит по внедорожью", 15000)
};
app.MapGet("/", () => repo);
app.MapPost("/add", (Bike o) => repo.Add(o));
app.MapPut("/{id}", (int id, UpdateDTO dto) =>
{
    Bike buffer = repo.Find(x => x.number == id);
    buffer.name = dto.name;
    buffer.description = dto.description;
    buffer.price = dto.price;
});
app.MapDelete("/bike/delete/{id}", (int id) =>
{
    Bike buffer = repo.Find(x => x.number == id);
    repo.Remove(buffer);
});
app.Run();

class Bike
{
    public int number { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int price { get; set; }
    public Bike(int number, string name, string description, int price)
    {
        this.number = number;
        this.name = name;
        this.description = description;
        this.price = price;
    }
}
record class UpdateDTO(string name, string description, int price);