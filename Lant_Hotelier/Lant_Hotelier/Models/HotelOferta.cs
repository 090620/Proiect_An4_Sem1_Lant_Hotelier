using Lant_Hotelier.Models;

public class HotelOferta
{
    public int ID_Hotel { get; set; }
    public Hotel Hotel { get; set; }

    public int ID_Oferta { get; set; }
    public Oferte Oferte { get; set; }
}