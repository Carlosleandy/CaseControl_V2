import PrimeVue from 'primevue/config'
import InputText from 'primevue/inputtext'
import InputIcon from 'primevue/inputicon'
import Button from 'primevue/button'
import ToastService from 'primevue/toastservice'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Row from 'primevue/row'
import Dialog from 'primevue/dialog'
import Toolbar from 'primevue/toolbar'
import Tooltip from 'primevue/tooltip'
import Sidebar from 'primevue/sidebar'
import MobileGridCard from '@/components/widgets/MobileGridCard.vue'
import ContentWrapper from '@/components/wrappers/ContentWrapper.vue'
import PaginationInfo from '@/components/widgets/PaginationInfo.vue'
import InputSearch from '@/components/form/InputSearch.vue'
import Skeleton from 'primevue/skeleton'
import Checkbox from 'primevue/checkbox'
import ConfirmationService from 'primevue/confirmationservice'
import ProgressBar from 'primevue/progressbar'
import ProgressSpinner from 'primevue/progressspinner'
import InputNumber from 'primevue/inputnumber'
import Dropdown from 'primevue/dropdown'
import Calendar from 'primevue/calendar'
import TextArea from 'primevue/textarea'
import Password from 'primevue/password'
import SelectButton from 'primevue/selectbutton'
import ToggleButton from 'primevue/togglebutton'
import IconField from 'primevue/iconfield'
import TabView from 'primevue/tabview';
import TabPanel from 'primevue/tabpanel';
import Accordion from 'primevue/accordion';
import AccordionTab from 'primevue/accordiontab';
import FileUpload from 'primevue/fileupload';
import MultiSelect from 'primevue/multiselect';
import Card from 'primevue/card';


const usePrimeVue = (app: any) => {
    app.use(PrimeVue);
    app.use(ToastService);
    app.component('IconField', IconField);
    app.component('InputIcon', InputIcon);
    app.component('InputText', InputText);
    app.component('PButton', Button);
    app.component('DataTable', DataTable);
    app.component('Column', Column);
    app.component('Row', Row);   
    app.component('Toolbar', Toolbar);
    app.component('PDialog', Dialog);      
    app.component('Sidebar', Sidebar);
    app.component('Skeleton', Skeleton);
    app.component('Checkbox', Checkbox);
    app.component('InputSearch', InputSearch);
    app.component('ContentWrapper', ContentWrapper);          
    app.component('MobileGridCard', MobileGridCard);  
    app.component('ProgressBar', ProgressBar);       
    app.component('ProgressSpinner', ProgressSpinner);  
    app.component('InputNumber', InputNumber);  
    app.component('PaginationInfo', PaginationInfo);
    app.component('Dropdown', Dropdown);            
    app.component('Calendar', Calendar);            
    app.component('TextArea', TextArea);
    app.component('Password', Password);  
    app.component('SelectButton', SelectButton);   
    app.component('ToggleButton', ToggleButton); 
    app.component('TabView', TabView); 
    app.component('Accordion', Accordion); 
    app.component('AccordionTab', AccordionTab); 
    app.component('FileUpload', FileUpload); 
    app.component('MultiSelect', MultiSelect); 
    app.component('Card', Card); 
            

    app.use(ConfirmationService);   
    app.directive('tooltip', Tooltip);
}

export default usePrimeVue;