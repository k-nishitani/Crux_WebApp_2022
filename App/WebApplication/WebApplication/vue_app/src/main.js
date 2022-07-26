import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

// OpenLayersのCSSを読み込み
import 'ol/ol.css';

createApp(App).use(store).use(router).mount('#app')
