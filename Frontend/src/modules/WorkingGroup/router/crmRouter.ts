
import workinggroupRepository from '../repository/workinggroupRepository'

export default [    
    {
        path:'workinggroup',
        name:'workinggroup',
        component: () => import('../ui/workinggroup/WorkingGroup.vue'),
        meta: {
            i18n: ['workinggroup']
        },
        props: {
            workinggroupRepository,
        }
    },
];
