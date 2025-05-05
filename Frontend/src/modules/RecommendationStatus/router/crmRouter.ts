
import recommendationstatusRepository from '../repository/recommendationstatusRepository'

export default [    
    {
        path:'recommendationstatus',
        name:'recommendationstatus',
        component: () => import('../ui/recommendationstatus/RecommendationStatus.vue'),
        meta: {
            i18n: ['recommendationstatus']
        },
        props: {
            recommendationstatusRepository,
        }
    },
];
