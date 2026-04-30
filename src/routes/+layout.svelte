<script>
	// Gemensam favicon för hela appen.
	import favicon from '$lib/assets/favicon.svg';

	// SvelteKit skickar in den aktiva sidans innehåll via children.
	let { children } = $props();
</script>

<svelte:head>
	<link rel="icon" href={favicon} />
</svelte:head>

<!-- Renderar den aktiva sidan inuti appens layout. -->
{@render children?.()}
