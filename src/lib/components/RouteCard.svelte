<script>
    import { city } from "$lib/stores/city.svelte";
    let { departure = "", arrival = "", delay = 0 } = $props();
</script>

<article class="route-card">
    <header class="card-header">
        <section class="time-info">
            <h2>{city.from}</h2>
            <time class="time">{departure}</time>
            <span class="label">DEPARTURE</span>
        </section>

        <div class="timeline">
            <span class="timeline-dot" aria-hidden="true"></span>
            <section class="timeline-details">
                <i class="fa-solid fa-train" aria-hidden="true"></i>
                <span class="duration">10   m</span>
                <span class="transfer">1 transfer</span>
            </section>
            <span class="timeline-dot" aria-hidden="true"></span>
        </div>

        <section class="time-info">
            <h2>{city.to}</h2>
            <time class="time">{arrival}</time>
            <span class="label">ARRIVAL</span>
        </section>

        <aside
                class="delay-tag {delay < 5 ? 'low'
                    : delay < 15
                  ? 'medium'
                  : 'high'}"
        >
            <i class="fa-solid fa-circle-exclamation" aria-hidden="true"></i>
            <span class="status">{delay > 0 ? "Delayed" : "On time"}</span>
            {#if delay > 0}
                <span class="minutes">{delay} min</span>
            {/if}
        </aside>
    </header>

    <nav class="route-details" aria-label="Route stops">
        <ul class="stops-list">
            <li class="stop">{city.from}</li>
            <li class="arrow">
                <i class="fa-solid fa-arrow-right" aria-hidden="true"></i>
            </li>
            <li class="stop">Mellan Station</li>
            <li class="arrow">
                <i class="fa-solid fa-arrow-right" aria-hidden="true"></i>
            </li>
            <li class="stop">{city.to}</li>
        </ul>
    </nav>
</article>

<style>
    :root {
        --primary-red: #d90000;
        --light-red: #ffe5e5;
        --dark-text: #333;
        --light-text: #666;
        --white: #fff;
        --border-color: #ccc;
        --light-bg: #f5f5f5;
    }

    article {
        background-color: var(--white);
        border-radius: 12px;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        width: 100%;
        max-width: 1000px;
        overflow: hidden;
        border-left: 8px solid var(--primary-red);
        margin: 1rem;
    }

    .card-header {
        display: flex;
        align-items: center;
        padding: 20px;
        position: relative;
        border-bottom: 1px solid #eee;
    }

    .time-info {
        display: flex;
        flex-direction: column;
        text-align: center;
        min-width: 80px;
    }

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
        margin-top: 5px;
    }

    .timeline {
        flex-grow: 1;
        display: flex;
        align-items: center;
        justify-content: space-between;
        margin: 0 15px;
        position: relative;
    }

    .timeline::before {
        content: "";
        position: absolute;
        top: 50%;
        left: 10px;
        right: 10px;
        height: 2px;
        background-color: var(--border-color);
        transform: translateY(-50%);
        z-index: 1;
    }

    .timeline-dot {
        width: 10px;
        height: 10px;
        background-color: var(--white);
        border: 2px solid var(--border-color);
        border-radius: 50%;
        z-index: 2;
    }

    .timeline-details {
        display: flex;
        flex-direction: column;
        align-items: center;
        background-color: var(--white);
        padding: 8px 12px;
        border: 1px solid var(--border-color);
        border-radius: 8px;
        text-align: center;
        z-index: 2;
    }

    .timeline-details i {
        font-size: 1.2em;
        color: var(--dark-text);
        margin-bottom: 2px;
    }

    .timeline-details .duration {
        font-size: 1.1em;
        font-weight: bold;
        color: var(--dark-text);
    }

    .timeline-details .transfer {
        font-size: 0.75em;
        color: var(--light-text);
        margin-top: 2px;
    }

    .delay-tag {
        background-color: var(--light-red);
        color: var(--primary-red);
        padding: 10px 15px;
        border-radius: 8px;
        font-weight: bold;
        display: flex;
        flex-direction: column;
        align-items: center;
        text-align: center;
        margin-left: 20px;
        min-width: 90px;
    }

    .delay-tag.low {
        background-color: #e6ffe6;
        color: #009900;
    }

    .delay-tag.medium {
        background-color: #fff3e0;
        color: #ff9800;
    }

    .delay-tag.high {
        background-color: #ffe5e5;
        color: #d90000;
    }

    .delay-tag i {
        font-size: 1.2em;
        margin-bottom: 3px;
    }

    .delay-tag .status {
        font-size: 0.9em;
        line-height: 1;
    }

    .delay-tag .minutes {
        font-size: 1.1em;
        font-weight: 700;
    }

    .route-details {
        display: flex;
        align-items: center;
        padding: 15px 20px;
        flex-wrap: wrap;
    }

    .stops-list {
        display: flex;
        align-items: center;
        gap: 10px;
        padding: 0;
        margin: 0;
        list-style: none;
        flex-wrap: wrap;
    }

    .stop {
        background-color: var(--light-bg);
        border-radius: 6px;
        padding: 8px 12px;
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
