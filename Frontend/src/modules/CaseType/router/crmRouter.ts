
import casetypeRepository from '../repository/casetypeRepository'

export default [    
    {
        path:'casetype',
        name:'casetype',
        component: () => import('../ui/casetype/CaseType.vue'),
        meta: {
            i18n: ['casetype']
        },
        props: {
            casetypeRepository,
        }
    },
];
