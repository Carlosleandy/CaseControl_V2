
import linkedRepository from '../repository/linkedRepository'

export default [    
    {
        path:'linkeds',
        name:'linked',
        component: () => import('../ui/linked/Linked.vue'),
        meta: {
            i18n: ['linked']
        },
        props: {
            linkedRepository,
        }
    },
];
