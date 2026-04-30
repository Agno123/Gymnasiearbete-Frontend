<script>
    // Resultatkort och delad state som används efter en sökning.
    import RouteCard from "$lib/components/RouteCard.svelte";
    import { city } from "$lib/stores/city.svelte";
    import { stops } from "$lib/stores/stop.svelte.js";
</script>

<!-- Visa resultatdelen först när en sökning har startat. -->
{#if city.searched}
    <div class="routes-container">
        <!-- Välj rätt resultatläge: laddar, fel, tomt eller kort. -->
        {#if stops.loadingRoutes}
            <p class="status-message">Söker resor...</p>
        {:else if stops.routeError}
            <p class="status-message error">{stops.routeError}</p>
        {:else if stops.routes.length === 0}
            <p class="status-message">Inga resor hittades.</p>
        {:else}
        <!-- Rendera ett resekort för varje resa som API:t returnerar. -->
        {#each stops.routes as route}
            <RouteCard
                departure={route.avgång}
                arrival={route.ankomst}
                duration={route.restid}
                routeStops={route.stopp}
                fromName={city.from}
                toName={city.to}
            />
        {/each}
        {/if}
    </div>
{/if}

<style>
    /* Radbryter resekorten och håller dem centrerade. */
    .routes-container {
        display: flex;
        flex-wrap: wrap;
        justify-content: center;
        width: 100%;
        padding: 0.625rem;
    }

    /* Gemensam stil för laddning, tomt läge och felmeddelanden. */
    .status-message {
        margin: 1rem;
        color: #333;
        font-weight: 600;
    }

    .status-message.error {
        color: #d90000;
    }
</style>
