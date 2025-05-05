
import userlevelRepository from '../repository/userlevelRepository'

export default [    
    {
        path:'userlevel',
        name:'userlevel',
        component: () => import('../ui/userlevel/UserLevel.vue'),
        meta: {
            i18n: ['userlevel']
        },
        props: {
            userlevelRepository,
        }
    },
];
