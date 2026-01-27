<script>
    import RouteCard from "$lib/components/RouteCard.svelte";
    import { city } from "$lib/stores/city.svelte";
    let routes = [
        { departure: "08:02", arrival: "08:12", delay: 4 },
        { departure: "09:15", arrival: "09:34", delay: 10 },
        { departure: "09:15", arrival: "09:34", delay: 15 },
    ];

 
</script>

{#if city.searched}
    <div class="routes-container">
        {#each routes as route}
            <RouteCard
                departure={route.departure}
                arrival={route.arrival}
                delay={route.delay}
            />
        {/each}
    </div>
{/if}

<style>
    .routes-container {
        display: flex;
        flex-wrap: wrap;
        justify-content: center;
        width: 100%;
        padding: 10px;
    }
</style>
