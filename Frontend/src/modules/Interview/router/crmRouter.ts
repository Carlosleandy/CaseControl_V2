
import interviewRepository from '../repository/interviewRepository'

export default [    
    {
        path:'interview',
        name:'interview',
        component: () => import('../ui/interview/Interview.vue'),
        meta: {
            i18n: ['interview']
        },
        props: {
            interviewRepository,
        }
    },
];
