<!--マップコンポーネント-->
<template>
  <div class="mapPane">
    <!--マップ表示-->
    <div id="map"></div>
  </div>
</template>

<script>
  // OpenLayersを読み込み
  import Map from 'ol/Map';
  import View from 'ol/View';
  import TileLayer from 'ol/layer/Tile';
  import OSM from 'ol/source/OSM';

import { fromLonLat } from 'ol/proj';

  export default {
    name: 'MapPane',
    mounted: function () {
      // マップオブジェクト生成
      this.mapCreate();
    },
    methods: {
      // マップオブジェクト生成
      mapCreate: function () {
        // 地図読み込み
        // new Map ({
        //     target: 'map',
        //     layers: [
        //         new TileLayer({
        //             source: new XYZ({
        //                 url: 'http://cyberjapandata.gsi.go.jp/xyz/std/{z}/{x}/{y}.png',
        //                 attributions: 'Maptiles by <a href="http://www.gsi.go.jp/kikakuchousei/kikakuchousei40182.html" target="_blank" rel="noopener noreferrer">国土地理院</a> contributors, under ODbL.',
        //                 attributionsCollapsible: false,
        //                 tileSize: [256, 256],
        //                 minZoom: 0,
        //                 maxZoom: 18
        //             })
        //         })
        //     ],
        //     view: new View ({
        //         center: fromLonLat([139.767, 35.681]),
        //         zoom: 11
        //     })
        // });
        new Map({
          target: 'map',
          layers: [
            new TileLayer({
              preload: 4,
              source: new OSM(),
            }),
          ],
          view: new View({
            center: fromLonLat([139.767, 35.681]),
            zoom: 11,
          }),
        });
      },
    },
  };
</script>

<style scoped>
  /*マップサイズ*/
  #map {
    z-index: 0;
    height: 800px;
  }
</style>
