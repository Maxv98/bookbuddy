<template>
  <div class="unique-svg-container" :class="color" :style="`height:${size.height}; width:${size.width};`" v-html="svgContent"/>
</template>

<script setup lang="ts">
import {GlobalTypes} from '@/types/type-collection';
import { PropType, ref, onMounted } from 'vue';

let props = defineProps({
  icon: {
    type: String as PropType<GlobalTypes.IconType>,
    default: GlobalTypes.IconType.Search
  },
  color: {
    type: String,
    default: "orange"
  },
  size: {
    type: Object as any,
    default: () => ({
      width: '1.5em',
      height: '1.5em'
    })
  }
})

const svgContent = ref('');

const fetchAndSetSvg = async () => {
  const response = await fetch(`/icons/${props.icon}.svg`);
  svgContent.value = await response.text();
};

onMounted(() => {
  fetchAndSetSvg();
})

</script>

<style scoped>
.unique-svg-container{
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>