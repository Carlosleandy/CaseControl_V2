
import userRepository from '../repository/userRepository'

export default [    
    {
        path:'user',
        name:'user',
        component: () => import('../ui/user/User.vue'),
        meta: {
            i18n: ['user']
        },
        props: {
            userRepository,
        }
    },
];
