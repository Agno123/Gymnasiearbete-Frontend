<script>
    import { stops } from '$lib/stores/stop.svelte.js'; // 
    let fromQuery = "";

    async function searchStops() {
        if (fromQuery.length < 2) return;

        try {
            const result = await fetch(
                `http://localhost:5113/api/stops?from=${fromQuery}`,
            );
            if (result.ok) {
                stops.list = await result.json();
            }
        } catch (err) {
            console.error("Fetch error:", err);
        }
    }
</script>

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
