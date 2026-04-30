<script>
    import { city } from "$lib/stores/city.svelte";
    import { stops } from "$lib/stores/stop.svelte.js";

    // Lokala inputvärden innan användaren skickar en resesökning.
    let fromQuery = $state("");
    let toQuery = $state("");

    // Hårdkodat resdatum som skickas till backendens rese-endpoint.
    const travelDate = "2025-10-04";

    async function searchFromStops() {
        // Vänta med API-anrop tills användaren har skrivit nog många tecken.
        if (fromQuery.length < 2) {
            stops.fromList = []; // Rensa listan om söksträngen är för kort
            return;
        }

        try {
            // Fråga backend efter matchande avgångshållplatser.
            const response = await fetch(
                `http://localhost:5113/api/stops?query=${encodeURIComponent(fromQuery)}`,
            );

            if (response.ok) {
                const data = await response.json();
                stops.fromList = data; // Uppdaterar svelte.js store
                city.searched = true;
            } else {
                console.error("Serverfel:", response.status);
            }
        } catch (err) {
            console.error("Kunde inte nå API:", err);
        }
    }

    async function searchToStops() {
        // Rensa gamla destinationsförslag när söktexten är för kort.
        if (toQuery.length < 2) {
            stops.toList = [];
            return;
        }

        try {
            // Fråga backend efter matchande ankomsthållplatser.
            const response = await fetch(
                `http://localhost:5113/api/stops?query=${encodeURIComponent(toQuery)}`,
            );

            if (response.ok) {
                const data = await response.json();
                stops.toList = data;
                city.searched = true;
            } else {
                console.error("Serverfel:", response.status);
            }
        } catch (err) {
            console.error("Kunde inte nå API:", err);
        }
    }

    async function searchJourney() {
        // Skicka inte en tom resesökning.
        if (!fromQuery.trim() || !toQuery.trim()) return;

        // Använd första API-förslaget, annars det användaren skrev.
        city.from = stops.fromList[0].name;
        city.to = stops.toList[0].name;
        city.searched = true;

        // Nollställ resultatvyn innan nya resor laddas.
        stops.loadingRoutes = true;
        stops.routeError = "";
        stops.routes = [];

        // Bygg query-parametrar i ett URL-vänligt format.
        const params = new URLSearchParams({
            startName: city.from,
            endName: city.to,
            date: travelDate,
        });

        try {
            // Hämta resalternativ mellan valda hållplatser.
            const response = await fetch(
                `http://localhost:5113/api/travel/time?${params.toString()}`,
            );

            if (response.ok) {
                stops.routes = await response.json();
            } else {
                stops.routeError = `Kunde inte hämta resor (${response.status}).`;
            }
        } catch (err) {
            stops.routeError = "Kunde inte nå API:t.";
            console.error("Kunde inte nå API:", err);
        } finally {
            stops.loadingRoutes = false;
        }
    }
</script>

<!-- Formuläret söker resa utan att ladda om sidan. -->
<form class="search-card" onsubmit={(e) => { e.preventDefault(); searchJourney(); }}>
    <div class="form-group">
        <label for="from">Från:</label>
        <input
            id="from"
            type="text"
            bind:value={fromQuery}
            oninput={searchFromStops}
            placeholder="Sök hållplats..."
            required
        />
    </div>
    <div class="form-group">
        <label for="to">Till:</label>
        <input
            id="to"
            type="text"
            bind:value={toQuery}
            oninput={searchToStops}
            placeholder="Ex. Lund"
            required
        />
    </div>
    <button type="submit">Sök resa</button>
</form>

<style>
    /* Delade färgvariabler för sökkortet. */
    :root {
        --primary-red: #d90000;
        --light-red: #ffe5e5;
        --dark-text: #333;
        --light-text: #666;
        --white: #fff;
        --border-color: #ccc;
        --light-bg: #f5f5f5;
    }

    .search-card {
        background-color: var(--white);
        border-radius: 0.75rem;
        box-shadow: 0 0.25rem 0.625rem rgba(0, 0, 0, 0.1);
        padding: 1.25rem;
        width: 100%;
        max-width: 37.5rem;
        margin: 1rem;
        display: flex;
        flex-direction: column;
        gap: 0.9375rem;
        border-left: 0.5rem solid var(--primary-red);
    }

    /* Staplar varje label direkt ovanför sitt inputfält. */
    .form-group {
        display: flex;
        flex-direction: column;
        gap: 0.3125rem;
    }

    label {
        font-size: 0.9em;
        font-weight: 500;
        color: var(--light-text);
    }

    /* Inputfält får en diskret fokusring när de är aktiva. */
    input {
        padding: 0.625rem 0.75rem;
        border: 0.0625rem solid var(--border-color);
        border-radius: 0.5rem;
        font-size: 1em;
        color: var(--dark-text);
        background-color: var(--light-bg);
        transition:
            border-color 0.2s ease,
            box-shadow 0.2s ease;
    }

    input:focus {
        outline: none;
        border-color: var(--primary-red);
        box-shadow: 0 0 0 0.1875rem rgba(217, 0, 0, 0.15);
    }

    /* Primär knapp som startar resesökningen. */
    button {
        align-self: flex-start;
        background-color: var(--primary-red);
        color: var(--white);
        padding: 0.75rem 1.25rem;
        border: none;
        border-radius: 0.5rem;
        font-size: 1em;
        font-weight: bold;
        cursor: pointer;
        transition:
            background-color 0.2s ease,
            transform 0.1s ease;
    }

    button:hover {
        background-color: #b80000;
    }

    button:active {
        transform: scale(0.97);
    }
</style>
