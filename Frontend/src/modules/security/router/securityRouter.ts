import UserRepository from '../repository/mock/mockUserRepository'
// import RoleRepository from '../repository/mock/mockRoleRepository'

export default [
    {
        path:'/users/:id?/:dialogType?',
        name:'users',
        component: () => import('../ui/user/User.vue'),
        meta: {
            i18n: ['security']
        },
        props: {
            UserRepository,
            // RoleRepository
        }
    },

    // {
    //     path:'/roles',
    //     name:'roles',
    //     component: () => import('../ui/role/Role.vue'),
    //     meta: {
    //         i18n: ['security']
    //     },
    //     props: {
    //         UserRepository,
    //         RoleRepository
    //     }
    // },
];
