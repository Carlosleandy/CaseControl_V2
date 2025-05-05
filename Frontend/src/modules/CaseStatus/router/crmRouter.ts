
import casestatusRepository from '../repository/casestatusRepository'

export default [    
    {
        path:'casestatus',
        name:'casestatus',
        component: () => import('../ui/casestatus/CaseStatus.vue'),
        meta: {
            i18n: ['casestatus']
        },
        props: {
            casestatusRepository,
        }
    },
];
