namespace RegistrarCedula.Shared
{
    public class Persona
    {
        public string Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Direction { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}