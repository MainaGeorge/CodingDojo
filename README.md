# CodingDojo

This is a project create an application that receives a three-letter code for a North American Country and returns a list of 
all countries a driver must travel through to go from the United State of America to the destination
The following assumptions were made when creating the app, using the following image:

![countries](https://user-images.githubusercontent.com/54276299/163845176-a054b101-5f29-4403-b223-f673364d8e76.png)

the countries with their three letter codes are:

![countrycodes](https://user-images.githubusercontent.com/54276299/163845827-51790b25-9b60-44db-bb81-c862fa21566c.png)

- The order in which the countries are saved to the database is the **same order** as they appear on the map from top to bottom
- The geography of the countries, i.e the map data is **not subject to change,** hence no need for any CRUD operations on the data
- Each country can have **ONLY ONE top neighbour**(neighbour to the north) through which one can travel to or from that country
- A country can be a **top neighbour to MORE THAN ONE other country** e.g from the image above, Mexico is the top neighbour to both Belize and Guatemala
- The path to countries that are adjacent to each other is not possible e.g it is not possible to go from Belize to Guatemala. In a future edition it will be implemented.
