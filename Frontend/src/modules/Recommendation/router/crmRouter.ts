
import recommendationRepository from '../repository/recommendationRepository'

export default [    
    {
        path:'recommendation',
        name:'recommendation',
        component: () => import('../ui/recommendation/Recommendation.vue'),
        meta: {
            i18n: ['recommendation']
        },
        props: {
            recommendationRepository,
        }
    },
];
