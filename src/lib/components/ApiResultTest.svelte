<script>
    // Tillfällig testkomponent som visar rå hållplatsdata.
    import { stops } from '$lib/stores/stop.svelte.js';

    // Lokal söktext som bara används av den här API-testhjälpen.
    let fromQuery = "";

    async function searchStops() {
        // Hoppa över backend-anrop när söktexten är för kort.
        if (fromQuery.length < 2) return;

        try {
            // Hämtar matchande hållplatser från den lokala backend-endpointen.
            const result = await fetch(
                `http://localhost:5113/api/stops?from=${fromQuery}`,
            );
            if (result.ok) {
                // Sparar svaret så listan nedanför kan rendera det.
                stops.list = await result.json();
            }
        } catch (err) {
            console.error("Fetch error:", err);
        }
    }
</script>

<!-- Enkel debuglista för hållplatsdata från searchStops. -->
<ul>
    {#each stops.list as stop}
        <li>
            <strong>{stop.name}</strong> (Läge {stop.platformCode})
            <br /><small
                >ID: {stop.id} - Datum: {new Date(
                    stop.date,
                ).toLocaleDateString()}</small
            >
        </li>
    {:else}
        <li>Inga hållplatser hittades.</li>
    {/each}
</ul>
