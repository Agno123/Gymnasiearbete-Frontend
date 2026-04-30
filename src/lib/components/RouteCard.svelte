<script>
    // Props skickas in från RouteResult för ett resekort.
    let {
        departure = "",
        arrival = "",
        duration = "",
        routeStops = [],
        fromName = "Stop",
        toName = "Destination",
    } = $props();

    function formatTime(time) {
        return time ? time.slice(0, 5) : "";
    }
</script>

<article class="route-card">
    <!-- Headern jämför avgångs- och ankomsttid på en tidslinje. -->
    <header class="card-header">
        <section class="time-info">
            <h2>{fromName}</h2>
            <time class="time">{formatTime(departure)}</time>
            <span class="label">DEPARTURE</span>
        </section>

        <!-- Tidslinjen i mitten visar tågikonen och total restid. -->
        <div class="timeline">
            <span class="timeline-dot" aria-hidden="true"></span>
            <section class="timeline-details">
                <i class="fa-solid fa-train" aria-hidden="true"></i>
                <span class="duration">{duration}</span>
            </section>
            <span class="timeline-dot" aria-hidden="true"></span>
        </div>

        <section class="time-info">
            <h2>{toName}</h2>
            <time class="time">{formatTime(arrival)}</time>
            <span class="label">ARRIVAL</span>
        </section>
    </header>

    <!-- Hållplatslistan visar bara start/slut om API:t saknar stoppdetaljer. -->
    <nav class="route-details" aria-label="Route stops">
        <ul class="stops-list">
            {#each routeStops.length > 0 ? routeStops : [fromName, toName] as stop, index}
                {#if index > 0}
                    <li class="arrow"><i class="fa-solid fa-arrow-right"></i></li>
                {/if}
                <li class="stop">{stop}</li>
            {/each}
        </ul>
    </nav>
</article>

<style>
    /* Lokala designvariabler för resekortet. */
    :root {
        --primary-red: #d90000;
        --light-red: #ffe5e5;
        --dark-text: #333;
        --light-text: #666;
        --white: #fff;
        --border-color: #ccc;
        --light-bg: #f5f5f5;
    }

    /* Huvudkortet för ett enskilt resresultat. */
    article {
        background-color: var(--white);
        border-radius: 0.75rem;
        box-shadow: 0 0.25rem 0.625rem rgba(0, 0, 0, 0.1);
        width: 100%;
        max-width: 62.5rem;
        overflow: hidden;
        border-left: 0.5rem solid var(--primary-red);
        margin: 1rem;
    }

    /* Överdelen håller starttid, tidslinje och sluttid i linje. */
    .card-header {
        display: flex;
        align-items: center;
        padding: 1.25rem;
        position: relative;
        border-bottom: 0.0625rem solid #eee;
    }

    /* Gemensam layout för start- och sluttidsblocken. */
    .time-info {
        display: flex;
        flex-direction: column;
        text-align: center;
        min-width: 5rem;
    }

    /* Stor klocktext för avgång och ankomst. */
    .time {
        font-size: 2.2em;
        font-weight: bold;
        color: var(--dark-text);
        line-height: 1;
    }

    .time-info .label {
        font-size: 0.7em;
        color: var(--light-text);
        font-weight: 500;
        margin-top: 0.3125rem;
    }

    /* Horisontell linje som visuellt kopplar ihop avgång och ankomst. */
    .timeline {
        flex-grow: 1;
        display: flex;
        align-items: center;
        justify-content: space-between;
        margin: 0 0.9375rem;
        position: relative;
    }

    .timeline::before {
        content: "";
        position: absolute;
        top: 50%;
        left: 0.625rem;
        right: 0.625rem;
        height: 0.125rem;
        background-color: var(--border-color);
        transform: translateY(-50%);
        z-index: 1;
    }

    /* Punkterna markerar början och slutet på tidslinjen. */
    .timeline-dot {
        width: 0.625rem;
        height: 0.625rem;
        background-color: var(--white);
        border: 0.125rem solid var(--border-color);
        border-radius: 50%;
        z-index: 2;
    }

    /* Mittrutan ligger ovanpå tidslinjen och visar restiden. */
    .timeline-details {
        display: flex;
        flex-direction: column;
        align-items: center;
        background-color: var(--white);
        padding: 0.5rem 0.75rem;
        border: 0.0625rem solid var(--border-color);
        border-radius: 0.5rem;
        text-align: center;
        z-index: 2;
    }

    .timeline-details i {
        font-size: 1.2em;
        color: var(--dark-text);
        margin-bottom: 0.125rem;
    }

    .timeline-details .duration {
        font-size: 1.1em;
        font-weight: bold;
        color: var(--dark-text);
    }

    /* Flexibel hållplatslista under tidslinjen. */
    .route-details {
        display: flex;
        align-items: center;
        padding: 0.9375rem 1.25rem;
        flex-wrap: wrap;
    }

    /* Håller hållplatser och pilar på en rad som kan brytas. */
    .stops-list {
        display: flex;
        align-items: center;
        gap: 0.625rem;
        padding: 0;
        margin: 0;
        list-style: none;
        flex-wrap: wrap;
    }

    /* Enskilda hållplatsbrickor. */
    .stop {
        background-color: var(--light-bg);
        border-radius: 0.375rem;
        padding: 0.5rem 0.75rem;
        font-size: 0.9em;
        font-weight: 500;
        color: var(--dark-text);
        white-space: nowrap;
    }

    li.arrow i {
        color: var(--light-text);
        font-size: 0.8em;
    }
</style>
