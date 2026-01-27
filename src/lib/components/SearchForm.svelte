<script>
    import { city } from "$lib/stores/city.svelte";

    let localFrom = "";
    let localTo = "";

    function search(e) {
        e.preventDefault();

        city.from = localFrom;
        city.to = localTo;
        city.searched = true;
    }
</script>

<form class="search-card" onsubmit={search}>
    <div class="form-group">
        <label for="from">Från:</label>
        <input
            id="from"
            type="text"
            bind:value={localFrom}
            placeholder="Ex. Malmö"
            required
        />
    </div>
    <div class="form-group">
        <label for="to">Till:</label>
        <input
            id="to"
            type="text"
            bind:value={localTo}
            placeholder="Ex. Lund"
            required
        />
    </div>
    <button type="submit">Sök resa</button>
</form>

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

    .search-card {
        background-color: var(--white);
        border-radius: 12px;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        padding: 20px;
        width: 100%;
        max-width: 600px;
        margin: 1rem;
        display: flex;
        flex-direction: column;
        gap: 15px;
        border-left: 8px solid var(--primary-red);
    }

    .form-group {
        display: flex;
        flex-direction: column;
        gap: 5px;
    }

    label {
        font-size: 0.9em;
        font-weight: 500;
        color: var(--light-text);
    }

    input {
        padding: 10px 12px;
        border: 1px solid var(--border-color);
        border-radius: 8px;
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
        box-shadow: 0 0 0 3px rgba(217, 0, 0, 0.15);
    }

    button {
        align-self: flex-start;
        background-color: var(--primary-red);
        color: var(--white);
        padding: 12px 20px;
        border: none;
        border-radius: 8px;
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
