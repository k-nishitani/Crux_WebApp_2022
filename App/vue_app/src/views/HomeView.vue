<template>
  <div class="aboutMap">
        <!--マップコンポーネント表示-->
        <MapPane></MapPane>
    </div>
</template>

<script>
// マップコンポーネント読み込み
    import MapPane from '@/components/MapPane.vue'

    export default {
        name: 'aboutMap',
        components: {
            MapPane
        }
    }
</script>
