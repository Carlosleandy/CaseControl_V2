
<script setup lang="ts">
	import { computed } from 'vue'
	import { useSystemStore } from '@/modules/system/store/system'
	import InfoCard from '@/components/widgets/InfoCard.vue'
	import 'sweetalert2/src/sweetalert2.scss'	
	import Swal from 'sweetalert2/dist/sweetalert2.js'
	
	let systemStore = useSystemStore();
	let theme = computed(() => systemStore.theme)
</script>

<template>
	<div>
		<section class="info-cards" style="--min: 60ch">

		</section>
	</div>
</template>

<style>
	.info-cards {
		display: flex;
		gap: 20px;
		margin-bottom: 20px;
	}

	.card-heart {
		h3 {
			min-width: 150px;
		}
	}

</style>@/modules/system/store/system