
import evidenceclassificationRepository from '../repository/evidenceclassificationRepository'

export default [    
    {
        path:'evidenceclassification',
        name:'evidenceclassification',
        component: () => import('../ui/evidenceclassification/EvidenceClassification.vue'),
        meta: {
            i18n: ['evidenceclassification']
        },
        props: {
            evidenceclassificationRepository,
        }
    },
];
