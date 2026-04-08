🚆 Skånetrafik Resplanerare (Fullstack)

En fullstack-applikation för att söka hållplatser och hitta direkta resor mellan stationer i Skåne, byggd med:

Backend: ASP.NET Core + MySQL
Frontend: SvelteKit
Data: GTFS (General Transit Feed Specification)
📦 Funktioner
🔍 Sök hållplatser (autocomplete)
🚏 Hantering av stationer och plattformar (ParentStation)
🧭 Hitta direkta resor mellan två stationer
⏱ Visar avgång, ankomst och restid
⚡ Optimerad SQL för snabbare queries
🧠 Datamodell (GTFS)

Projektet bygger på följande tabeller:

Stops
StopTimes
Trips
Routes
Relationer
Stop (station)
  ↓ ParentStation
Stop (plattform/läge)
  ↓ StopId
StopTimes
  ↓ TripId
Trips
  ↓ RouteId
Routes
⚙️ Backend (ASP.NET Core)
📁 Struktur
Controllers/
StopsController
TripsController
Services/
StopsService
Models/
Stop, Trip, Route, StopTime etc.
🚀 Starta backend
Installera beroenden
Lägg in connection string i appsettings.json:
{
  "ConnectionStrings": {
    "SkaneTrafikDb": "server=localhost;user=root;password=xxx;database=skanetrafik;"
  }
}
Kör projektet:
dotnet run

Backend startar på t.ex:

http://localhost:5113
📡 API Endpoints
🔍 Sök hållplatser
GET /api/stops?query=malmö

Returnerar:

[
  {
    "id": "9021012080000000",
    "name": "Malmö C",
    "parentStation": "",
    "locationType": "1"
  }
]
🚆 Hitta resa
GET /api/trips?fromParentStation=ID&toParentStation=ID

Exempel:

http://localhost:5113/api/trips?fromParentStation=9021012080000000&toParentStation=9021012080040000
⚡ Prestanda

För att undvika långsamma queries används:

filtrering tidigt (ParentStation → StopTimes)
JOIN endast på relevant data
index i databasen
🔧 Rekommenderade index
CREATE INDEX idx_stops_parent_date_stopid
ON Stops (ParentStation, date, StopId);

CREATE INDEX idx_stoptimes_stopid_date_tripid_id
ON StopTimes (StopId, date, TripId, Id);

CREATE INDEX idx_stoptimes_tripid_date_id
ON StopTimes (TripId, date, Id);

CREATE INDEX idx_trips_tripid_date_routeid
ON Trips (TripId, date, RouteId);

CREATE INDEX idx_routes_routeid_date
ON Routes (RouteId, date);
🎨 Frontend (SvelteKit)
Funktioner
🔎 Live-sökning av hållplatser
👆 Val av station
🔁 Skicka station-ID till backend
📊 Visa resa
🚀 Starta frontend
npm install
npm run dev

Startar på:

http://localhost:5173
🔗 CORS (Backend)

Backend tillåter frontend via:

.WithOrigins("http://localhost:5173")
🔄 Dataimport (GTFS)

Projektet innehåller importer för:

routes.txt
trips.txt
stops.txt
stop_times.txt
Viktigt
Alla tabeller måste ha samma date per import
ParentStation används för att koppla station ↔ plattform
StopTimes används för att hitta resor
