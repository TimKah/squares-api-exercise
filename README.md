# Squares API Exercise
## Description
This API checks if a collection of points is a part of the hollow or solid square.

Example:

![sq](https://github.com/TimKah/squares-api-exercise/assets/23475498/9b4f02de-0c4a-46f5-9367-f199d1239723)

Red dots - outside of the square

Yellow dots - on the square border, part of the hollow and solid square

Green dots - inside of the square, part of the solid square


Considerations for the future:
* Implement DB to store information
* Implement authorization & authentication
* Implement proper API testing

## How to run
First, you have to go to the app settings and choose memory type. Currently there are only 2 options:
 * Memory storage (everything would be stored in the program itself)
 * Redis database

For memory storage choose "MemoryStorage", for Redis database choose "RedisDatabase"

There are 2 options to launch:
 * Open Powershell and run the file "run.ps1" in the root (works only with memory storage)
 * Open the command line and enter "docker compose up --build" (you have to have an installed Docker) and open "http://localhost:8000/swagger/index.html"
