// Delad rese- och hållplatsdata för sökformuläret och resultatkomponenterna.
export const stops = $state({
    // Förslag på avgångshållplatser från backend.
    fromList: [],

    // Förslag på destinationer från backend.
    toList: [],

    // Resalternativ som returneras efter en sökning.
    routes: [],

    // UI-status för resultatdelen.
    loadingRoutes: false,
    routeError: ""
});
