<script setup lang="ts">
	import { ref, onMounted } from 'vue'

	const props = defineProps<{
        searching: boolean
    }>();

	const slots = defineSlots();
	const showExtraButtons = ref(false);
	const openExtraButtons = ref(false);
	const showButtons = ref(false);
	const emit = defineEmits(['onContentFinish']);
	let timer: null | ReturnType<typeof setTimeout> = null;
	let lastScrollTop = 0;
	let loading = false;

	if(slots['mobile-extra-buttons']) {
		const extraButtons = slots['mobile-extra-buttons']();
		showExtraButtons.value = extraButtons.length > 0;
	}

	if(slots['mobile-buttons']) {
		const buttons = slots['mobile-buttons']();
		showButtons.value = buttons.length > 0;
	}
	
	// Performing mobile
	const getIsMobile = () => document.documentElement.clientWidth < 770;
	
	const isMobile = ref(getIsMobile());
	
	window.addEventListener('resize', () => {
		const currentStatus = getIsMobile();
		if (isMobile.value !== currentStatus) {
			setTimeout(() => {
				listenScollChange(currentStatus ? '.mobile-card-wrapper' : '.p-datatable-wrapper');
			}, 200);
		}

		isMobile.value = getIsMobile();
	}, true);

	const checkExtraButtonClick = () => {
		const buttons = document.querySelectorAll('.mobile-extra-buttons button');
		buttons.forEach((e) => {
			e.addEventListener('click', ()=>{
				openExtraButtons.value = false;
			});
		});
	}

	const onIntercept = () => {
		emit('onContentFinish');
	}

	function isScrolledBottom(element: HTMLElement) {
		const scrollPosition = element.scrollTop;
		const scrollableHeight = element.scrollHeight - element.clientHeight;
		const ninetyFivePercentHeight = scrollableHeight * 0.95;
		return scrollPosition >= ninetyFivePercentHeight;
	}

	function checkScrollDirectionIsUp(element: HTMLElement) {
		var currentTop = element.scrollTop;
		const result = currentTop > lastScrollTop;
		lastScrollTop = currentTop <= 0 ? 0 : currentTop;
		return result;
	}

	const listenScollChange = (selector: string) => {
		const element = document.querySelector(selector);		

		if(!element) return;

		element.addEventListener('scroll', function handleScrollEvent(e) {
			if(props.searching || !isScrolledBottom(e.target as HTMLElement)) {
				return;
			}

			if (!checkScrollDirectionIsUp(e.target as HTMLElement)) {
				return;
			}

			if(!loading) {
				onIntercept();
				loading = true;
			}

			if(timer != null) clearTimeout(timer);

			timer = setTimeout(() => {
				loading = false;
			}, 1000);
		});
	}

	onMounted(()=>{
		checkExtraButtonClick();
		listenScollChange(isMobile.value ? '.mobile-card-wrapper' : '.p-datatable-wrapper');
	});
</script>

<template>
	<div class="desktop" v-if="!isMobile">
		<slot name="desktop"></slot>
	</div>

	<div class="mobile mobile-component" v-if="isMobile">
		<slot name="mobile"></slot>
	</div>
	
	<div class="content-block" @click="openExtraButtons = !openExtraButtons" v-bind:class="{ 'show' : openExtraButtons}"></div>

	<div class="mobile-buttons-container" v-if="isMobile && showButtons">
		<div class="mobile-extra-buttons" v-bind:class="{ 'open' : openExtraButtons}">
			<slot name="mobile-extra-buttons"></slot>
		</div>

		<div class="mobile-buttons">
			<slot name="mobile-buttons"></slot>
			<PButton @click="openExtraButtons = !openExtraButtons" text aria-label="more" v-if="showExtraButtons">
				<i class="pi pi-ellipsis-h"></i>
				<span>Más</span>
			</PButton> 		
		</div>
	</div>

</template>

<style lang="scss">
	$menu-height: 80px;

	.mobile-card-wrapper {
		padding-bottom: $menu-height;
		max-height: calc(100vh - 200px);
		overflow: auto;
		padding-top: 10px;
	}

	.desktop table tbody {
		max-height: 600px;
		overflow: auto;
	}

	.mobile-buttons-container {
		position: fixed;
		bottom: 0px;
		left: 0px;
		width: 100%;
		height: $menu-height;
		z-index: 9;
		border-top: 1px solid var(--border-color);
	}

	.mobile-buttons {
		display: flex;
		z-index: 9;
		width: 100%;
		justify-content: space-around;
		position: absolute;
		background: var(--surface-ground);

		.p-button {
			display: flex;
			flex-direction: column;
			justify-content: center;
			align-items: center;
			gap: 5px;
			height: $menu-height;
			font-size: 12px;
			width: 20%;
		}

		span {
			height: 50%;
			overflow: none;
			text-overflow: ellipsis;
		}
		
		i {
			font-size: 20px;
			height: 50%;
		}
	}

	.mobile-extra-buttons{
		background: var(--surface-card);
		position: absolute;
		width: 100%;
		left: 0;
		transform: translateY(100%);
		transition: .3s;
		justify-content: left;
		display: flex;
		flex-wrap: wrap;
		z-index: 8;
		grid-template-columns: repeat(auto-fill, minmax(82px, 1fr));
		grid-auto-rows: 74px;
		grid-auto-flow: dense;

		&.open {
			transform: translateY(-100%);
		}

		.p-button {
			display: flex;
			flex-direction: column;
			justify-content: center;
			align-items: center;
			gap: 5px;
			height: $menu-height;
			font-size: 12px;
			width: 20%;
		}

		i {
			font-size: 25px;
			height: 50%;
		}
	}

	.content-block {
		position: fixed;
		left: 0;
		height: 100%;
		background: rgba(0, 0, 0, .7);
		width: 100%;
		top: 0;
		z-index: 8;
		visibility: hidden;
		opacity: 0;
		transition: visibility .3s linear, opacity .3s linear;

		&.show {
			visibility: visible;
			opacity: 1;
		}
	}
</style>