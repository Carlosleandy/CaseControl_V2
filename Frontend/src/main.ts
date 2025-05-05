import './assets/main.scss'
import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import { useRouter } from './router'
import usePrimeVue from './plugins/prime-vue'
import useInternalization from './plugins/i18n'
import useDirective from './plugins/custome_directives/directives'
import useDatePipe from './plugins/pipes/date'

import sweetalert2 from 'sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';

import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';
//import 'bootstrap/dist/css/bootstrap.css';

const app = createApp(App);
app.use(createPinia());
//app.use(sweetalert2);
app.use(VueSweetalert2);

usePrimeVue(app);
useInternalization(app);
useRouter(app);
useDirective(app);
useDatePipe(app);
app.mount('#app');

import 'bootstrap/dist/js/bootstrap'
