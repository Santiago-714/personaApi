namespace personaApi.Model
{
    public class Person
    {
        public int Id { get; set; } //Creado escribiendo prop + TAB
        public string Name { get; set; } = string.Empty; //Empty inicializa en vacio
        public string Lastname { get; set; }= string.Empty;
        public string Email { get; set; }= string.Empty;
        public int Age { get; set; }

    }
}