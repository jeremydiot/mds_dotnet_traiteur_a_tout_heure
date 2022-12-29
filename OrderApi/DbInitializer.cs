public static class DbInitializer
{
  public static void Initialize(OrderDbContext context)
  {

    context.Database.EnsureCreated();

    var clients = new Client[]{
      new Client{Id=1, Firstname="John",Lastname= "Smith",Address= "123 Main Street, Anytown, USA"},
      new Client{Id=2, Firstname="Mary",Lastname= "Johnson",Address= "456 Oak Avenue, Anytown, USA"},
      new Client{Id=3, Firstname="David",Lastname= "Williams",Address= "789 Birch Road, Anytown, USA"},
      new Client{Id=4, Firstname="Sarah",Lastname= "Jones",Address= "321 Maple Street, Anytown, USA"},
      new Client{Id=5, Firstname="Michael",Lastname= "Brown",Address= "159 Pine Avenue, Anytown, USA"},
      new Client{Id=6, Firstname="Laura",Lastname= "Davis",Address= "357 Cedar Boulevard, Anytown, USA"},
      new Client{Id=7, Firstname="Jose",Lastname= "Garcia",Address= "963 Spruce Lane, Anytown, USA"},
      new Client{Id=8, Firstname="Christina",Lastname= "Rodriguez",Address= "214 Willow Drive, Anytown, USA"},
      new Client{Id=9, Firstname="Robert",Lastname= "Martin",Address= "647 Oak Street, Anytown, USA"},
      new Client{Id=10,Firstname="Jennifer",Lastname= "Anderson",Address= "159 Maple Avenue, Anytown, USA"},
    };

    foreach (Client c in clients)
    {
      context.Clients.Add(c);
    }
    context.SaveChanges();
  }

}