
import faultRepository from '../repository/faultRepository'

export default [    
    {
        path:'faults',
        name:'MENU.faults',
        component: () => import('../ui/fault/Fault.vue'),
        meta: {
            i18n: ['faults']
        },
        props: {
            faultRepository,
        }
    },
];
