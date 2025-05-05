
import receptionmediumRepository from '../repository/receptionmediumRepository'

export default [    
    {
        path:'receptionmedium',
        name:'receptionmedium',
        component: () => import('../ui/receptionmedium/ReceptionMedium.vue'),
        meta: {
            i18n: ['receptionmedium']
        },
        props: {
            receptionmediumRepository,
        }
    },
];
