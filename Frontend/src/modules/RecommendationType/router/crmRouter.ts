
import recommendationtypeRepository from '../repository/recommendationtypeRepository'

export default [    
    {
        path:'recommendationtype',
        name:'recommendationtype',
        component: () => import('../ui/recommendationtype/RecommendationType.vue'),
        meta: {
            i18n: ['recommendationtype']
        },
        props: {
            recommendationtypeRepository,
        }
    },
];
