
import roleRepository from '../repository/roleRepository'

export default [    
    {
        path:'role',
        name:'role',
        component: () => import('../ui/role/Role.vue'),
        meta: {
            i18n: ['roles']
        },
        props: {
            roleRepository,
        }
    },
];
