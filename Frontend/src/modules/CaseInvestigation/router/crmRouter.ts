
import caseinvestigationRepository from '../repository/caseinvestigationRepository'

export default [    
    {
        path:'caseinvestigation',
        name:'caseinvestigation',
        component: () => import('../ui/caseinvestigation/CaseInvestigation.vue'),
        meta: {
            i18n: ['caseinvestigation']
        },
        props: {
            caseinvestigationRepository,
        }
    },
];
