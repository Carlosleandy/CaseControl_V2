
import binnacleRepository from '../repository/binnacleRepository'

export default [    
    {
        path:'binnacle',
        name:'binnacle',
        component: () => import('../ui/binnacle/Binnacle.vue'),
        meta: {
            i18n: ['binnacle']
        },
        props: {
            binnacleRepository,
        }
    },
];
