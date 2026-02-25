<script>
    import SearchForm from "$lib/components/SearchForm.svelte";
    import RouteCard from "$lib/components/RouteCard.svelte";
    import RouteResult from "$lib/components/RouteResult.svelte";
    import ApiResultTest from "$lib/components/ApiResultTest.svelte";


    import { fade, fly, scale } from "svelte/transition";
    import { city } from "$lib/stores/city.svelte";

    import "$lib/app.css";


</script>

    <h1>Sök din resa enkelt och snabbt</h1>
    <p>Fyll i vart du vill åka och hitta bästa resalternativen direkt.</p>

    <SearchForm />
    <RouteResult />
    <ApiResultTest />

<style>
</style>
